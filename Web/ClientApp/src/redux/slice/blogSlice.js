import { BlogsClient } from '../../web-api-client.ts';
import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';

export const fetchBlogData = createAsyncThunk('blog/fetchBlogData',
    async () => {
        var client = new BlogsClient();
        const response = await client.getBlogList();
        return response.toJSON();
    }
)

export const blogSlice = createSlice({
    name: 'blog',
    initialState: { data: [], loading: false, error: null, blogWithId: null },
    extraReducers: (builder) => {
        builder.addCase(fetchBlogData.pending, (state) => {
            state.loading = true;
        })
            .addCase(fetchBlogData.fulfilled, (state, action) => {
                state.loading = false;
                state.data = action.payload;
                state.error = '';
            })
            .addCase(fetchBlogData.rejected, (state, action) => {
                state.loading = false;
                state.error = action.error.message;
                state.data = [];
            });
    }
});
export default blogSlice.reducer;
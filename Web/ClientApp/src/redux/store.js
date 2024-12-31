import { configureStore } from '@reduxjs/toolkit'
import todosReducer from './slice/todosSlice'
import blogReducer from './slice/blogSlice'

const store = configureStore({
    reducer: {
        todos: todosReducer,
        blog: blogReducer
    }
})

export default store;
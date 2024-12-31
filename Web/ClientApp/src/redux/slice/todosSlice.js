import { createSlice } from "@reduxjs/toolkit";
const initialState = { id: 20, todoLists: [{ title: "Bir deneme" },{title:"İki deneme"}] }
 const todosSlice = createSlice({
    name:'todos',
    initialState,
    reducers: {
        todoAdded(state, action) {
            state.push({
                id: action.payload.id,
                text: action.payload.text,
                completed:false
            })
        },
        todoToggled(state, action) {
            const todo = state.find(todo => todo.id === action.payload)
            todo.completed = !todo.completed
        },
        todoDeleteItem(state, action) {
            let newList = state.todoLists.filter(x => x.title !== action.title)
            state.todoLists = newList;
        }
    }
})

export const { todoAdded, todoToggled, todoDeleteItem } = todosSlice.actions
export default todosSlice.reducer
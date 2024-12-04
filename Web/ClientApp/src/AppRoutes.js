import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import { TodoLists } from "./components/TodoLists";
import { Navigate } from 'react-router-dom';
const AppRoutes = [
    //{
    //  path: "/",
    //  element: <Navigate to="/counter" />
    //},
    {
        path: "/",
        element: <Home />
    },
    {
        index: true,
        path: '/counter',
        element: <Counter />
    },
    {
        path: '/fetch-data',
        element: <FetchData />
    },
    {
        path: '/todo-list',
        element: <TodoLists />
    }
];

export default AppRoutes;

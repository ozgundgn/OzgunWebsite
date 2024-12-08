import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import { TodoLists } from "./components/TodoLists";
import { Description } from "./components/Description"
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
    },
    {
        path: '/todo-list/:title',
        element:<Description/>
    }
];

export default AppRoutes;

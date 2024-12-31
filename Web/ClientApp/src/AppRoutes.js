import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import { TodoLists } from "./components/TodoLists";
import { Description } from "./components/Description"
import BlogDetail from "./components/blog/BlogDetail"
import Blog from "./components/blog/Blog";
import Project from "./components/project/Project";
import {Contact} from "./components/contact/Contact";
import { AboutMe } from "./components/aboutme/AboutMe";
import { Cv } from "./components/cv/Cv";

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
        element: <Description />
    },
    {
        path: '/blog/:id',
        element: <BlogDetail />
    },
    {
        path: '/blog',
        element: <Blog />
    },
    {
        path: '/projects',
        element: <Project />
    },
    {
        path: '/contact',
        element: <Contact />
    },
    {
        path: '/aboutme',
        element: <AboutMe />
    },
    {
        path: '/cv',
        element: <Cv />
    }
];

export default AppRoutes;

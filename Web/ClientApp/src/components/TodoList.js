import React from 'react';
import { Button } from 'reactstrap';
import { Link } from 'react-router-dom';

export const TodoList = ({ title, description, id, fillSelected }) => {
    const selectButtonClickHandler = () => {
        fillSelected({ title, description, id });
    };
    return (
        <div className="card">
            <p hidden>{id}</p>
            <h1> <Link to={"/todo-list/" + title}> Title: {title} </Link></h1>
            <h3>Description: <span>{description}</span>  </h3>
            <Button onClick={selectButtonClickHandler}>Select</Button>
        </div>
    )
}
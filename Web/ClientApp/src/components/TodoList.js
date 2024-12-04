import React from 'react';
import { Button } from 'reactstrap';

export const TodoList = ({ title, description, id, fillSelected }) => {
    const selectButtonClickHandler = () => {
        fillSelected({ title, description, id });

        console.log("sdfsfsfsefsesefsefs hhghhghghghhghghghhghg");
    };
    return (
        <div className="card">
            <p hidden>{id}</p>
            <h1>Title: {title} </h1>
            <h3>Description: <span>{description}</span>  </h3>
            <Button onClick={selectButtonClickHandler}>Select</Button>
        </div>
    )
}
import React from 'react';
import { TodoList } from './TodoList';

export function TodoListList({ todoLists, fillSelected }) {
  return (
      todoLists.map(todo => {
          return <TodoList title={todo.title} description={todo.description} id={todo.id} key={todo.id} fillSelected={fillSelected} />;
    })
  );
}
import React, { Component } from 'react';
import { Button } from 'reactstrap';
import { Route, Routes } from 'react-router-dom';
import AppRoutes from './AppRoutes';
import { connect } from 'react-redux';
import { Layout } from './components/Layout';
import './custom.css';

class App extends Component {
    static displayName = App.name;

    render() {
        const { id, todoLists } = this.props;
        const deleteClickHandler = (data, e) => {
            this.props.deleteItem(data);
        }

        let contents = todoLists.map(x => {
            return (
                <div className="card" key={x.title}>
                    <h1> Tite: {x.title}</h1>
                    <Button onClick={(e) => deleteClickHandler(x.title, e)}>Delete</Button>
                </div>
            )
        }
        )

        return (
            <div>
                <Layout>
                    <Routes>
                        {AppRoutes.map((route, index) => {
                            const { element, ...rest } = route;
                            return <Route key={index} {...rest} element={element} />;
                        })}
                    </Routes>
                </Layout>
                {/*<h1>Benim idm {id} </h1>*/}
                {/*{contents}*/}
            </div>

        );
    }
}
const mapStateToProps = (state) => {
    return {
        id: state.todos.id,
        todoLists: state.todos.todoLists
    }
}
const mapDispatchToProps = (dispatch) => {
    return {
        deleteItem: (title) => { dispatch({ type: "todos/todoDeleteItem", title: title }) }
    }
}
export default connect(mapStateToProps, mapDispatchToProps)(App);
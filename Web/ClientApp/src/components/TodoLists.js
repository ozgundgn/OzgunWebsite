import React, { Component } from 'react';
import { TodoListList } from './TodoListList';
import { CreateTodoListCommand, TodoListsClient } from '../web-api-client.ts';
import { FormGroup, Input, Label, Form, Button } from 'reactstrap';

export class TodoLists extends Component {
    static displayName = TodoLists.name;

    componentDidMount() {
        this.populateToDoListData();
    }

    constructor(props) {
        super(props);
        this.state = {
            title: "",
            description: "",
            loading: true,
            todoLists: [],
        };
    }
    fillSelected = (data) => {
        console.log(data.id, data.title, data.description);
        this.setState({
            title: data.title,
            id: data.id,
            description: data.description
        })
    }

    renderTodoListTable(todoLists) {
        return (
            <div>
                <TodoListList todoLists={todoLists} fillSelected={this.fillSelected}></TodoListList>
            </div>
        );
    }

    submitHandler = (e) => {
        e.preventDefault();
        this.saveTodoList();
    };
    clearButtonHandler = (e) => {
        e.preventDefault();
        this.setState({
            title: "",
            description: "",
            id: ""
        })
    }
    async saveTodoList() {
        let client = new TodoListsClient();
        if (this.state.id == "") {
            var newId = await client.createTodoList(new CreateTodoListCommand({
                title: this.state.title,
                description: this.state.description
            }));
            let newTodoListItem = {
                title: this.state.title,
                id: newId,
                description: this.state.description
            };
            let newTodoList = [...this.state.todoLists, newTodoListItem];
            this.setState({ todoLists: newTodoList });
        } else {

        }
    };

    titleChangeHandler(e) {
        this.setState({ title: e.target.value });
    }

    descriptionChangeHandler(e) {
        this.setState({ description: e.target.value });
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderTodoListTable(this.state.todoLists);
        return (
            <div key="todoListDiv">
                <h1 id="tableLabel">Todo Lists</h1>
                <p>This component demonstrates fetching data from the todo list api.</p>
                <div>
                    <Form onSubmit={(e) => this.submitHandler(e)}>
                        <FormGroup>
                            <Label hidden={this.state.id != "" ? false : true} >You are about to update this {this.state.id}</Label>
                            <Label for="todoListTitle">
                                Title
                            </Label>
                            <Input
                                id="todoListTitle"
                                name="title"
                                placeholder="enter a title"
                                onChange={(e) => this.titleChangeHandler(e)}
                                value={this.state.title}
                            />
                        </FormGroup>
                        <FormGroup>
                            <Label for="colour">
                                Description
                            </Label>
                            <Input
                                id="todoListDescription"
                                name="description"
                                placeholder="enter a description"
                                onChange={(e) => this.descriptionChangeHandler(e)}
                                value={this.state.description}
                            />
                        </FormGroup>
                        <Button>
                            Submit
                        </Button>
                        <Button onClick={this.clearButtonHandler}>
                            Clear
                        </Button>
                    </Form>
                </div>
                {contents}
            </div>
        );
    }

    async populateToDoListData() {
        let client = new TodoListsClient();
        var data = await client.getTodoLists();
        this.setState({ todoLists: data.lists, loading: false })
    }
}
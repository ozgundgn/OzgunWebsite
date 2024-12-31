import React, { Component } from 'react';
import { Card, CardTitle, CardText, CardBody, Input, FormText, FormGroup, Form, FormFeedback, Label, CardHeader, Button, CardImg } from 'reactstrap';
import 'bootstrap/dist/css/bootstrap.css';
import './Home.css';
import { GoArrowUpRight } from "react-icons/go";
import { Dialog } from 'primereact/dialog';
import { Button as ButtonPrime } from 'primereact/button';
import { Link } from 'react-router-dom';


export class Home extends Component {
    static displayName = Home.name;
    visible = false;

    constructor(props) {
        super(props);
        this.state = { visible: false, email: "", error: false, message: "" };
        this.show = this.show.bind(this);
        this.setVisible = this.setVisible.bind(this);
    }

    show = () => {
        this.setVisible(true);
    };

    setVisible = (visible) => {
        this.setState({
            visible: visible,
        })
    }
    handleSendMessage = (e) => {
        e.preventDefault();
        if (!e)
            this.setState({
                error: true
            });
        else
            this.setState({
                error: false
            });
    }
    handleMessageChange = (e) => {
        if (e.target.value !== undefined && e.target.value !== "")
            this.state.setState({
                message: e.target.value
            });
    }
    handleEmailChange = (e) => {
        this.setState({
            email: e.target.value
        })
    }
    footerContent = (
        <div>
            <ButtonPrime label="No" icon="pi pi-times" onClick={() => this.setVisible(false)} className="p-button-text" />
            <ButtonPrime label="Yes" icon="pi pi-check" onClick={() => this.setVisible(false)} autoFocus />
        </div>
    );
    render() {

        return (
            <div className="home-page">
                <div className="logo">
                    <span className="logo-name">
                        Özgün S. DOĞAN
                    </span>
                </div>
                <div className="row">
                    <div className="col-md-6 banner-image">
                        <Card
                            body
                            className="text-center home-page-card">
                            <CardTitle className="software-developer" tag="h1">
                                Software Developer
                            </CardTitle>
                            <CardText className="sub-content">
                                Modern teknolojiler ile <br />
                                yaratıcı çözümler üretiyorum.
                            </CardText>
                            <CardText className="featured-techs">
                                Öne Çıkan Teknolojiler
                            </CardText>
                            <div className="d-inline-flex text-center mb-5 justify-content-center">
                                <div className="icon-dotnet"></div>
                                <div className="icon-c-sharp"></div>
                                <div className="icon-sql"></div>
                                <div className="icon-react"></div>
                            </div>
                            <div className="text-center">
                                <Link to={"/projects"} className="btn btn-outline-secondary base-btn home-page-btn-1 m-1">
                                    Projects
                                    <GoArrowUpRight />
                                </Link>
                                <Link to={"/contact"} className="btn btn-outline-secondary base-btn home-page-btn-2 m-1">
                                    Contact
                                    <GoArrowUpRight />
                                </Link>
                            </div>
                        </Card>
                    </div>
                    <div className="col-md-6">
                        <div className="home-bg-2"></div>
                    </div>
                    <div className="row">
                        <div className="col-md-6">
                            <div hidden={this.state.visible} onClick={() => this.show('bottom-right')} className="contact-me">Contact Me<br /><i className="bi bi-chat-quote"></i></div>
                            <div hidden={this.state.visible} className="contact-me-back"></div>
                            <Dialog
                                position='bottom-right'
                                visible={this.state.visible}
                                modal={false}
                                onHide={() => { if (!this.state.visible) return; this.setVisible(false); }}
                                content={({ hide }) => (
                                    <Card>
                                        <CardHeader>
                                            <CardImg
                                                alt=""
                                                src="./images/ppozguncut.jpeg"
                                                style={{
                                                    height: 40,
                                                    width: 40,
                                                    borderRadius: 30
                                                }}
                                                top
                                                width="100%"
                                            />
                                        </CardHeader>
                                        <Form className="mx-2">
                                            <FormGroup>
                                                <Label for="email">
                                                    Email
                                                </Label>
                                                <Input type="email" id="email" value={this.state.email} onChange={(e) => this.handleEmailChange(e)} invalid={this.state.error} />
                                                <FormFeedback hidden={!this.state.error }>
                                                    Please enter valid email!
                                                </FormFeedback>
                                                <Label for="message">
                                                    Message
                                                </Label>
                                                <Input id="message" type="textarea" label="message" onChange={(e) => this.handleMessageChange(e)}></Input>
                                                <FormText>
                                                    Example help text that remains unchanged.
                                                </FormText>
                                                <div className="flex align-items-center gap-2">
                                                    <Button size="sm m-1" onClick={(e) => this.handleSendMessage(e)}>Send</Button>
                                                    <Button size="sm m-1" onClick={(e) => hide(e)}>Cancel</Button>
                                                </div>
                                            </FormGroup>
                                        </Form>
                                    </Card>
                                    //<Card style={{
                                    //    width: '18rem'
                                    //}}>
                                    //        <div className="inline-flex flex-column gap-2">
                                    //            <label htmlFor="username" className="text-primary-50 font-semibold">
                                    //                Password
                                    //            </label>
                                    //            <Input id="password" label="Password"></Input>
                                    //        </div>
                                    //        <div className="inline-flex flex-column gap-2">
                                    //            <label htmlFor="username" className="text-primary-50 font-semibold">
                                    //                Message
                                    //            </label>
                                    //            <Input id="username" type="textarea" label="Username" className="bg-white-alpha-20 border-none p-3 text-primary-50"></Input>
                                    //        </div>
                                    //        <div className="flex align-items-center gap-2">
                                    //            <Button onClick={(e) => hide(e)}>Send</Button>
                                    //            <Button onClick={(e) => hide(e)}>Cancel</Button>
                                    //        </div>
                                    //    </Card>
                                )}
                            ></Dialog>
                        </div>
                    </div>
                </div>
                {/*<Container>*/}
                {/*    <div className="d-flex flex-row mb-3">*/}
                {/*        <div className="flex-column m-4">*/}
                {/*            <div className="main-news-card">*/}
                {/*            </div>*/}
                {/*            <span className="main-news-span">*/}
                {/*                What is the .Net?*/}
                {/*            </span>*/}
                {/*        </div>*/}
                {/*        <div className="flex-column m-4">*/}
                {/*            <div className="main-news-card">*/}
                {/*            </div>*/}
                {/*            <span className="main-news-span">*/}
                {/*                What is the .Net?*/}
                {/*            </span>*/}
                {/*        </div>*/}
                {/*        <div className="flex-column m-4">*/}
                {/*            <div className="main-news-card">*/}
                {/*            </div>*/}
                {/*            <span className="main-news-span">*/}
                {/*                What is the .Net?*/}
                {/*            </span>*/}
                {/*        </div>*/}
                {/*    </div>*/}
                {/*</Container>*/}
            </div>
        );
    }
}

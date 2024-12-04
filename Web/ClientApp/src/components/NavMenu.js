import React, { Component } from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-icons/font/bootstrap-icons.css';
import { Nav, Collapse, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';

export class NavMenu extends Component {
    static displayName = NavMenu.name;

    constructor(props) {
        super(props);

        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.navIconHover = this.navIconHover.bind(this)
        this.state = {
            collapsed: true,
            navMenuHidden:true
        };
    }

    toggleNavbar() {
        this.setState({
            collapsed: !this.state.collapsed,
        });
    }

    navIconHover() {
        this.setState({
            navMenuHidden: !this.state.navMenuHidden
        })
    }

    render() {
        return (
            <div style={{ position: "fixed", right: 0, marginTop: "17%", backgroundColor: 'white',zIndex:1}}>
                <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow" container light>
                    {/* <NavbarBrand tag={Link} to="/">GioWebsite.Web</NavbarBrand>*/}
                    <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
                    <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
                        <ul className="navbar-nav flex-grow">
                            <Nav tabs vertical onMouseOver={this.navIconHover} >
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/"><i className="bi bi-twitter-x"></i></NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark"><i className="bi bi-file-earmark-person"></i></NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark"><i className="bi bi-feather"></i></NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink className="text-dark"><i className="bi bi-folder2-open"></i> </NavLink>
                                </NavItem>
                                {/*<NavItem className="download-cv">*/}
                                {/*    <a className="nav-link text-dark" href="/Identity/Account/Manage">Account</a>*/}
                                {/*</NavItem>*/}
                            </Nav>
                            <Nav tabs vertical hidden={this.state.navMenuHidden} >
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/counter">Counter</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/fetch-data">Fetch Data</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} className="nav-link text-dark" to="/todo-list">Todo List</NavLink>
                                </NavItem>
                                {/*<NavItem className="download-cv">*/}
                                {/*    <a className="nav-link text-dark" href="/Identity/Account/Manage">Account</a>*/}
                                {/*</NavItem>*/}
                            </Nav>
                        </ul>
                    </Collapse>
                </Navbar>
            </div>
        );
    }
}

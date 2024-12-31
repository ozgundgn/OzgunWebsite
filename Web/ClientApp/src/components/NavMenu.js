import React, { Component } from 'react';
import 'bootstrap-icons/font/bootstrap-icons.css';
import 'bootstrap/dist/css/bootstrap.css';
import { Nav, Collapse, Navbar, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';

export class NavMenu extends Component {
    static displayName = NavMenu.name;

    constructor(props) {
        super(props);

        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.navIconHover = this.navIconHover.bind(this);

        this.state = {
            collapsed: true,
            navMenuHidden: true,
            collapsedWait: true,
            activeMenu: "home"
        };
    }

    toggleNavbar() {
        if (!this.state.collapsed) {
            this.setState({
                collapsed: !this.state.collapsed,
            });
            setTimeout(() => {
                this.setState({
                    collapsedWait: this.state.collapsed,
                });
            }, 500);
        } else {
            this.setState({
                collapsedWait: !this.state.collapsed,
                collapsed: !this.state.collapsed,
            });
        }
    }
    changeActiveMenu(menu) {
        this.setState({
            activeMenu: menu
        })

    }
    navIconHover() {
        this.setState({
            navMenuHidden: !this.state.navMenuHidden
        })
    }

    render() {
        return (

            <div className="navmenu-div">
                <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom border-top box-shadow" light>
                    {/* <NavbarBrand tag={Link} to="/">GioWebsite.Web</NavbarBrand>*/}
                    <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
                    <Collapse className="d-sm-inline-flex flex-sm-row-reverse my-3" isOpen={!this.state.collapsed} navbar>
                        <ul className="navbar-nav flex-grow my-1">
                            <Nav tabs vertical onMouseOver={this.navIconHover} hidden={!this.state.collapsedWait} >
                                <NavItem className={this.state.activeMenu === "home" ? "active-menu" : ""} >
                                    <NavLink tag={Link} className="text-dark" to="/"><div className="home-icon"></div></NavLink>
                                </NavItem>
                                <NavItem className={this.state.activeMenu === "blog" ? "active-menu" : ""}>
                                    <NavLink tag={Link} className="text-dark"><div className="blog-icon"></div></NavLink>
                                </NavItem>
                                <NavItem className={this.state.activeMenu === "projects" ? "active-menu" : ""}>
                                    <NavLink tag={Link} className="text-dark"><div className="project-icon"></div></NavLink>
                                </NavItem>
                                <NavItem className={this.state.activeMenu === "aboutme" ? "active-menu" : ""}>
                                    <NavLink className="text-dark"><div className="about-icon"></div></NavLink>
                                </NavItem>
                                <NavItem className={this.state.activeMenu === "contact" ? "active-menu" : ""}>
                                    <NavLink className="text-dark"><div className="contact-icon"></div></NavLink>
                                </NavItem>
                                <NavItem className={this.state.activeMenu === "cv" ? "active-menu" : ""}>
                                    <NavLink className="text-dark"><div className="download-cv-icon"></div></NavLink>
                                </NavItem>
                                {/*<NavItem className="download-cv">*/}
                                {/*    <a className="nav-link text-dark" href="/Identity/Account/Manage">Account</a>*/}
                                {/*</NavItem>*/}
                            </Nav>
                            <Nav tabs vertical onMouseLeave={this.navIconHover} hidden={this.state.navMenuHidden && this.state.collapsed} >
                                <NavItem onClick={(e) => this.changeActiveMenu("home")}>
                                    <NavLink tag={Link} className="text-dark home" to="/" >Home</NavLink>
                                </NavItem>
                                <NavItem onClick={(e) => this.changeActiveMenu("blog")} >
                                    <NavLink tag={Link} className="text-dark" to="/blog">Blog</NavLink>
                                </NavItem>
                                <NavItem onClick={(e) => this.changeActiveMenu("projects")}>
                                    <NavLink tag={Link} className="text-dark" to="/projects">Projects</NavLink>
                                </NavItem>
                                <NavItem onClick={(e) => this.changeActiveMenu("aboutme")}>
                                    <NavLink tag={Link} className="nav-link text-dark" to="/aboutme">About Me</NavLink>
                                </NavItem>
                                <NavItem onClick={(e) => this.changeActiveMenu("contact")}>
                                    <NavLink tag={Link} className="nav-link text-dark" to="/contact">Contact</NavLink>
                                </NavItem>
                                <NavItem onClick={(e) => this.changeActiveMenu("cv")}>
                                    <NavLink tag={Link} className="nav-link text-dark" to="/cv">CV</NavLink>
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

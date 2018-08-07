import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import { TextField, Button } from '@material-ui/core';
import { Register } from '../api/UserApi';

interface State {
    userName: string,
    password: string,
}

export default class Home extends React.Component<RouteComponentProps<{}>, State> {
    constructor(props: any, context: any) {
        super(props);

        this.state = {
            userName: '',
            password: '',
        }

        this.handleChange = this.handleChange.bind(this);
        this.submit = this.submit.bind(this);
    }

    handleChange = (key: string) => (event: any) => {
        this.setState({ [key]: event.target.value } as State);
    }

    submit() {
        Register({ UserName: this.state.userName, Password: this.state.password });
    }

    public render() {
        const { userName, password } = this.state;

        return <div>
            <TextField
                label="UserName"
                value={userName}
                onChange={this.handleChange("userName")}
            />
            <TextField
                label="Password"
                value={password}
                onChange={this.handleChange("password")}
            />

            <Button
                onClick={this.submit}
            >Register!
            </Button>>    
        </div>;
    }
}

import { IUserRequest } from "../interfaces/IUserRequest";

export function Register(request: IUserRequest) {
    fetch('https://localhost:44325/api/Users/Register', {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(request)
    });
}
import {model} from "./model.js";
import { updateView } from "./main.js";
export function authenticateUser(email, password) {
    const foundUser = model.users.find(
        (u) => (u.userEmail === email || u.userName === email) && u.password === password
    );

    if (foundUser) {
        model.app.loggedInUser = foundUser.id;
        model.inputs.login.error = '';

        if (foundUser.isAdmin) {
            model.app.currentPage = 'adminDashboard';
        } else {
            model.app.currentPage = 'homeLibrary';
        }
    } else {
        model.inputs.login.error = 'Incorrect username or password';
    }
    updateView();
}

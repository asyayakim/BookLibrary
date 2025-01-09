export const model = {
    app: {
        currentPage: "login",
        loggedInUser: null,
    },
    inputs: {
        login: {
            email: "",
            password: "",
            error: "",
        },
    },
    users: [
        {
            id: 1,
            userName: "admin",
            userEmail: "admin@example.com",
            password: "admin123",
            isAdmin: true,
        },
        {
            id: 2,
            userName: "user1",
            userEmail: "user1@example.com",
            password: "user123",
            isAdmin: false,
        },
    ],
};

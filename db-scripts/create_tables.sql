-- Creates the tables needed for Sartre.

CREATE TABLE users (
    login varchar (32) PRIMARY KEY,
    name varchar (255) NOT NULL UNIQUE,
    password varchar (255) NOT NULL,
    biography varchar (2048),
    website varchar (1024)
);

CREATE TABLE roles (
    id serial PRIMARY KEY,
    name varchar (255) NOT NULL UNIQUE
);

CREATE TABLE user_roles (
    user_login varchar (32) REFERENCES users (login),
    role_id integer REFERENCES roles (id)
);
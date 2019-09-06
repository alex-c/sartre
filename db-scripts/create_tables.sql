-- Creates the tables needed for Sartre.

-- 1/3 users and roles

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
    user_login varchar (32) REFERENCES users (login) NOT NULL,
    role_id integer REFERENCES roles (id) NOT NULL
);

INSERT INTO roles (name) VALUES ('admin');
INSERT INTO roles (name) VALUES ('author');

-- 2/3 blogs and posts

CREATE TABLE blogs (
    id varchar (32) PRIMARY KEY,
    title varchar (255) NOT NULL UNIQUE,
    description varchar (2048) NOT NULL
);

CREATE TABLE posts (
    id varchar (32) PRIMARY KEY,
    title varchar (255) NOT NULL UNIQUE,
    published boolean DEFAULT false,
    blog_id varchar (32) NOT NULL,
    content text NOT NULL
);

CREATE TABLE blog_contributors (
    blod_id varchar (32) REFERENCES blogs (id) NOT NULL,
    user_login varchar (32) REFERENCES users (login) NOT NULL
);

CREATE TABLE post_authors (
    post_id varchar (32) REFERENCES posts (id) NOT NULL,
    user_login varchar (32) REFERENCES users (login) NOT NULL
);

-- 3/3 Sartre config

CREATE TABLE sartre_config (
    default_blog_id varchar (32) REFERENCES blogs (id)
);


-- Sets up the 'sartre' database and user. Change the password before executing.

DROP DATABASE IF EXISTS sartre;
DROP ROLE IF EXISTS sartre;
CREATE ROLE sartre LOGIN PASSWORD 'password'; --change 'password' to desired password
CREATE DATABASE sartre OWNER sartre;
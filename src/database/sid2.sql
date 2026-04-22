CREATE TABLE app_user(
	usr_pk	INT IDENTITY(1,1) NOT NULL
		CONSTRAINT PK_user PRIMARY KEY,
	usr_type NVARCHAR(20) NOT NULL,
	usr_name NVARCHAR(100) NOT NULL,
	usr_fname NVARCHAR(50) NULL,
	usr_lname NVARCHAR(50) NULL,
	usr_email NVARCHAR(256) NOT NULL,
	usr_phone NVARCHAR(25) NULL
);

CREATE TABLE login_for_users (
login_pk	INT IDENTITY(1,1) NOT NULL
	CONSTRAINT PK_login PRIMARY KEY,
usr_fk		INT NOT NULL,
usr_usrname	NVARCHAR(50) NOT NULL,
usr_pword	NVARCHAR(256) NOT NULL,

-- Contraints
CONSTRAINT UQ_login_username UNIQUE (usr_usrname),
CONSTRAINT UQ_login_usr_fk UNIQUE (usr_fk),
CONSTRAINT FK_login_user FOREIGN KEY (usr_fk)
	REFERENCES app_user(usr_pk)
	ON UPDATE CASCADE
	ON DELETE CASCADE
);

-- Contraints
CONSTRAINT UQ_login_username UNIQUE (usr_usrname),
CONSTRAINT UQ_login_usr_fk UNIQUE (usr_fk),
CONSTRAINT FK_login_user FOREIGN KEY (usr_fk)
	REFERENCES app_user(usr_pk)
	ON UPDATE CASCADE
	ON DELETE CASCADE
);

CREATE TABLE dbo.store
(
	store_pk			INT IDENTITY(1,1) NOT NULL
		CONSTRAINT PK_store PRIMARY KEY,
	store_name			NVARCHAR(200) NOT NULL,
	store_address		NVARCHAR(200) NULL,
	store_city			NVARCHAR(100) NULL,
	store_state			CHAR(2) NULL,
	store_zip			NVARCHAR(10) NULL,
	store_phone			NVARCHAR(25) NULL,

	created_by			INT NOT NULL,
	previous_edit_by	INT NULL,

	CONSTRAINT FK_store_created_by
		FOREIGN KEY (created_by) REFERENCES dbo.app_user(usr_pk)
			ON UPDATE NO ACTION ON DELETE NO ACTION,
	CONSTRAINT FK_store_prev_edit_by
		FOREIGN KEY (previous_edit_by) REFERENCES dbo.app_user(usr_pk)
			ON UPDATE NO ACTION ON DELETE NO ACTION
);

CREATE TABLE dbo.school_info
(
	sch_info_pk			INT IDENTITY(1,1) NOT NULL
		CONSTRAINT PK_school_info PRIMARY KEY,
	sch_name			NVARCHAR(200) NOT NULL,
	sch_address			NVARCHAR(200) NULL,
	sch_city			NVARCHAR(100) NULL,
	sch_state			CHAR(2) NULL,
	sch_zip				NVARCHAR(10) NULL,
	sch_tax             DECIMAL(9,4) NULL,
	sch_website 		NVARCHAR(512) NULL,
	sch_bandwebsite		NVARCHAR(512) NULL,

	sch_start_date		DATE NULL,
	sch_end_date		DATE NULL,
	sch_sprbrk_start 	DATE NULL,
	sch_sprbrk_end		DATE NULL,

	created_by			INT NOT NULL,
	previous_edit_by	INT NULL,

	CONSTRAINT FK_schinfo_created_by
		FOREIGN KEY (created_by) REFERENCES dbo.app_user(usr_pk)
			ON UPDATE NO ACTION ON DELETE NO ACTION,
	CONSTRAINT FK_schinfo_prev_edit_by
		FOREIGN KEY (previous_edit_by) REFERENCES dbo.app_user(usr_pk)
			ON UPDATE NO ACTION ON DELETE NO ACTION
);

CREATE TABLE dbo.school
(
	school_pk			INT IDENTITY(1,1) NOT NULL
		CONSTRAINT PK_school PRIMARY KEY,

	sch_info_fk			INT NOT NULL,
	store_fk			INT NULL,
	created_by			INT NOT NULL,

	CONSTRAINT FK_school_sch_info
		FOREIGN KEY (sch_info_fk) REFERENCES dbo.school_info(sch_info_pk)
			ON UPDATE NO ACTION ON DELETE NO ACTION,

	CONSTRAINT FK_school_store
        FOREIGN KEY (store_fk)    REFERENCES dbo.store(store_pk)
            ON UPDATE NO ACTION ON DELETE NO ACTION,

    CONSTRAINT FK_school_created_by
        FOREIGN KEY (created_by)  REFERENCES dbo.app_user(usr_pk)
            ON UPDATE NO ACTION ON DELETE NO ACTION,

    CONSTRAINT UQ_school_sch_info UNIQUE (sch_info_fk)
);

CREATE TABLE dbo.director
(
	dir_pk			INT IDENTITY(1,1) NOT NULL
		CONSTRAINT PK_director PRIMARY KEY,

	school_fk		INT NOT NULL,
	dir_fname		NVARCHAR(100) NOT NULL,
	dir_lname		NVARCHAR(100) NOT NULL,
	dir_email		NVARCHAR(256) NULL,
	dir_phone		NVARCHAR(25) NULL,
	dir_notes		NVARCHAR(4000) NULL,

	dir_image		VARBINARY(MAX) NULL,

	created_by		INT NOT NULL,
	previous_edit_by	INT NULL,

	CONSTRAINT FK_director_school
		FOREIGN KEY (school_fk) REFERENCES dbo.school(school_pk)
			ON UPDATE NO ACTION ON DELETE CASCADE,

	CONSTRAINT FK_director_created_by
		FOREIGN KEY (created_by) REFERENCES dbo.app_user(usr_pk)
			ON UPDATE NO ACTION ON DELETE NO ACTION,
	
	CONSTRAINT FK_director_prev_edit_by
		FOREIGN KEY (previous_edit_by) REFERENCES dbo.app_user(usr_pk)
			ON UPDATE NO ACTION ON DELETE NO ACTION
);

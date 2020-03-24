CREATE TABLE Virus (
	`id` BIGINT auto_increment NOT NULL,
	`owner` BIGINT NOT NULL,
	`name` varchar(100) NOT NULL,
	`seed` varchar(100) NOT NULL,
	`damage` FLOAT NOT NULL,
	`projectile_speed` FLOAT NOT NULL,
	`speed` FLOAT NOT NULL,
	`life_points` FLOAT NOT NULL,
	`image` varchar(100) DEFAULT NULL,
	UNIQUE KEY `Virus_UN` (`seed`),
	CONSTRAINT Virus_PK PRIMARY KEY (`id`),
	CONSTRAINT Virus_FK FOREIGN KEY (`owner`) REFERENCES Account(`id`)
)
ENGINE=InnoDB;

-- Database setup for MariaDb 10.4

DROP DATABASE IF EXISTS repair_db;
CREATE DATABASE repair_db;

-- Reset and then create new tables

DROP TABLE IF EXISTS repair_db.RepairTasks;
CREATE TABLE repair_db.RepairTasks(
	TaskId INT NOT NULL AUTO_INCREMENT,
	DeviceId INT NOT NULL,
	DateAdded DATETIME NOT NULL,
	Description TEXT,
	Criticality INT NOT NULL,
	Completed INT NOT NULL,
	PRIMARY KEY(TaskId)
	);

DROP TABLE IF EXISTS repair_db.FactoryDevices;
CREATE TABLE repair_db.FactoryDevices(
	Id INT NOT NULL AUTO_INCREMENT,
	Name VARCHAR(255) NOT NULL,
	Year INT NOT NULL,
	Type VARCHAR(255) NOT NULL,
    PRIMARY KEY(Id)
	);

-- IMPORT/LOAD FROM CSV

LOAD DATA INFILE 'E:\ServiceManual/servicemanual-csharp/seeddata.csv' -- Path to file to read
INTO TABLE repair_db.FactoryDevices
FIELDS TERMINATED BY ','
IGNORE 1 LINES -- Ignore column headers
(Name, Year, Type);

LOAD DATA INFILE 'E:\ServiceManual/servicemanual-csharp/seeddata2.csv' -- Path to file to read
INTO TABLE repair_db.RepairTasks
FIELDS TERMINATED BY ','
IGNORE 1 LINES 
(TaskId, DeviceId, DateAdded, DESCRIPTION, Criticality, Completed);
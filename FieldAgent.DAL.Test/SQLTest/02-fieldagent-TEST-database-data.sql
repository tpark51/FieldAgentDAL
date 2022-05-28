--AGENCY
INSERT INTO Agency (ShortName, LongName)
    VALUES
    ('CIA','Central Intelligence Agency'),
    ('FBI','Federal Bureau of Investigation'),
    ('SHIELD','Strategic Homeland Intervention Enforcement and Logistics Division');

--LOCATION
INSERT INTO [Location] (AgencyId, LocationName, Street1, City, PostalCode, CountryCode)
    VALUES
    (1, 'CIA Headquarters', '1000 Colonial Farm Road', 'McLean','20505','US'),
    (2,'FBI Headquarters','935 Pennsylvania Ave NW','Washington','20535','US'),
    (3, 'SHIELD Headquarters','219 West 47th Street','New York','10001','US');

--AGENT
INSERT INTO AGENT (FirstName, LastName, DateOfBirth, Height)
    VALUES
    ('Aaron', 'Hotchner', '1970-11-02', 73),
    ('Allayne',	'Stephens',	'1948-12-26',61),
    ('Amil', 'Lottrington', '1988-08-06', 63),
    ('Emilly',	'Prentiss',	'1970-10-12', 67);

--ALIAS
INSERT INTO Alias (AgentId, AliasName)
    VALUES (4, 'Lauren Reynolds');


--CLEARANCE
INSERT INTO SecurityClearance (SecurityClearanceName)
    VALUES 
    ('BlackOps'),
    ('Retired'),
    ('Secret'),
    ('Top Secret'),
    ('None');

--MISSION
INSERT INTO Mission (AgencyId, CodeName, StartDate, ProjectedEndDate, ActualEndDate, OperationalCost)
    VALUES
    (1, 'Laced', '1998-05-15', '2000-05-11', '2000-06-23', 6703061.11),
    (2, '200', '2014-02-05', '2014-02-10', '2014-02-08', 2536732.32);


--AGENCYAGENT
INSERT INTO AgencyAgent (AgencyId, AgentId, SecurityClearanceId, ActivationDate, DeactivationDate, IsActive)
    VALUES 
    (2, 1, 4, '1992-08-01', '2016-05-23', 0 ),
    (1,	2,	2, '1971-03-15', '2001-11-15', 0),
    (2,	3,	4,	'2005-10-22', NULL, 1),
    (2, 4, 1, '1998-04-07', NULL, 1);

--MISSIONAGENT
INSERT INTO MissionAgent (AgentId, MissionId)
    VALUES
    (1, 2),
    (4,2);
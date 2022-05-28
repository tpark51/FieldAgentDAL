CREATE PROCEDURE [SetKnownGoodState]
AS
BEGIN
    ALTER TABLE [Location] DROP CONSTRAINT fk_Location_AgencyId
    ALTER TABLE Mission DROP CONSTRAINT fk_Mission_AgencyId
    ALTER TABLE Alias DROP CONSTRAINT fk_Alias_AgentId
    ALTER TABLE MissionAgent DROP CONSTRAINT fk_MissionAgent_MissionId
    ALTER TABLE MissionAgent DROP CONSTRAINT fk_MissionAgent_AgentId
    ALTER TABLE AgencyAgent DROP CONSTRAINT fk_AgencyAgent_AgencyId
    ALTER TABLE AgencyAgent DROP CONSTRAINT fk_AgencyAgent_AgentId
    ALTER TABLE AgencyAgent DROP CONSTRAINT fk_AgencyAgent_SecurityClearanceId

    TRUNCATE TABLE AgencyAgent;
    TRUNCATE TABLE MissionAgent;
    TRUNCATE TABLE [Location];
    TRUNCATE TABLE Mission;
    TRUNCATE TABLE Alias;
    TRUNCATE TABLE Agency;
    TRUNCATE TABLE Agent;
    TRUNCATE TABLE SecurityClearance;

    ALTER TABLE [Location] ADD CONSTRAINT fk_Location_AgencyId
        FOREIGN KEY (AgencyId)
        REFERENCES Agency(AgencyId)
    ALTER TABLE Mission ADD CONSTRAINT fk_Mission_AgencyId
            FOREIGN KEY (AgencyId)
        REFERENCES Agency(AgencyId)
    ALTER TABLE Alias ADD CONSTRAINT fk_Alias_AgentId
            FOREIGN KEY (AgentId)
        REFERENCES Agent(AgentId)
    ALTER TABLE MissionAgent ADD CONSTRAINT fk_MissionAgent_MissionId
            foreign key (MissionId)
        references Mission(MissionId)
    ALTER TABLE MissionAgent ADD CONSTRAINT fk_MissionAgent_AgentId
            foreign key (AgentId)
        references Agent(AgentId)
    ALTER TABLE AgencyAgent ADD CONSTRAINT fk_AgencyAgent_SecurityClearanceId
        foreign key (SecurityClearanceId)
        references SecurityClearance(SecurityClearanceId)
    ALTER TABLE AgencyAgent ADD CONSTRAINT fk_AgencyAgent_AgencyId
        foreign key (AgencyId)
        references Agency(AgencyId)
    ALTER TABLE AgencyAgent ADD CONSTRAINT fk_AgencyAgent_AgentId
        foreign key (AgentId)
         references Agent(AgentId)


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


END
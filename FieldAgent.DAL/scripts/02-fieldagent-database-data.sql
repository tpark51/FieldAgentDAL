use FieldAgent;

SELECT * FROM Temp;

--AGENCY
SELECT DISTINCT agency_short_name, agency_long_name
FROM TEMP;

INSERT INTO Agency (ShortName, LongName)
    SELECT DISTINCT agency_short_name, agency_long_name
    FROM TEMP;

select * from Agency;

--LOCATION
SELECT DISTINCT a.AgencyId, t.location_name, t.street1, t.street2, t.city, t.PostalCode, t.countryCode
    from Temp t
    inner join Agency a on t.agency_short_name = a.ShortName; 

INSERT INTO Location (AgencyId, LocationName, Street1, Street2, City, PostalCode, CountryCode)
    SELECT DISTINCT a.AgencyId, t.location_name, t.street1, t.street2, t.city, t.PostalCode, t.countryCode
    from Temp t
    inner join Agency a on t.agency_short_name = a.ShortName;

select * from location;

--AGENT
SELECT DISTINCT agent_first, agent_last, agent_dob, agent_height
from Temp;

INSERT INTO AGENT (FirstName, LastName, DateOfBirth, Height)
    SELECT DISTINCT agent_first, agent_last, agent_dob, agent_height
    FROM Temp;

SELECT * FROM AGENT;

--ALIAS
SELECT DISTINCT a.AgentId, t.alias_name
    from Temp t
    inner join Agent a on t.agent_first = a.FirstName
    inner join Agency ay on t.agency_short_name = ay.ShortName;

INSERT INTO Alias (AgentId, AliasName)
SELECT DISTINCT a.AgentId, t.alias_name
    from Temp t
    inner join Agent a on t.agent_first = a.FirstName
    inner join Agency ay on t.agency_short_name = ay.ShortName;

SELECT * FROM Alias;

--CLEARANCE
SELECT DISTINCT clearance
    from Temp;

INSERT INTO SecurityClearance (SecurityClearanceName)
    SELECT DISTINCT clearance
    from Temp;

SELECT * FROM SecurityClearance;

--MISSION
SELECT DISTINCT a.AgencyId, t.CodeName, t. mission_start_date, t.mission_proj_end, t.mission_act_end, t.mission_cost
    from Temp t
    inner join Agency a on t.agency_short_name = a.ShortName;

INSERT INTO Mission (AgencyId, CodeName, StartDate, ProjectedEndDate, ActualEndDate, OperationalCost)
    SELECT DISTINCT a.AgencyId, t.CodeName, t. mission_start_date, t.mission_proj_end, t.mission_act_end, t.mission_cost
    from Temp t
    inner join Agency a on t.agency_short_name = a.ShortName;

SELECT * FROM MISSION;

--AGENCYAGENT
select a.AgencyId, at.AgentId, sc.SecurityClearanceId, t.ActivationDate, t.DeactivationDate, t.IsActive
from Temp t
inner join Agency a on t.agency_short_name = a.ShortName
inner join Agent at on t.agent_first = at.FirstName
inner join SecurityClearance sc on t.clearance = sc.SecurityClearanceName;

INSERT INTO AgencyAgent (AgencyId, AgentId, SecurityClearanceId, ActivationDate, DeactivationDate, IsActive)
    select a.AgencyId, at.AgentId, sc.SecurityClearanceId, t.ActivationDate, t.DeactivationDate, t.IsActive
    from Temp t
    inner join Agency a on t.agency_short_name = a.ShortName
    inner join Agent at on t.agent_first = at.FirstName
    inner join SecurityClearance sc on t.clearance = sc.SecurityClearanceName;

select * from AgencyAgent;

--MISSIONAGENT
SELECT a.AgentId, m.MissionId
from Temp t 
inner join Agent a on t.agent_first = a.FirstName
inner join Mission m on t.CodeName = m.CodeName

INSERT INTO MissionAgent (AgentId, MissionId)
    SELECT a.AgentId, m.MissionId
    from Temp t 
    inner join Agent a on t.agent_first = a.FirstName
    inner join Mission m on t.CodeName = m.CodeName;

select * from MissionAgent;
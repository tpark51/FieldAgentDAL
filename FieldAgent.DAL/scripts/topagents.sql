ALTER PROCEDURE TopAgents
AS
BEGIN
    SELECT TOP(3)
    a.LastName + ', ' + a.FirstName as 'NameLastFirst',
    a.DateOfBirth,
    count(m.MissionId) as 'CompletedMissionCount'
FROM AGENT a
INNER JOIN MissionAgent ma on a.AgentId = ma.AgentId
INNER JOIN Mission m on ma.MissionId = m.MissionId
where m.ActualEndDate is not null
GROUP BY a.FirstName, a.LastName, a.DateOfBirth
ORDER BY count(m.MissionId) DESC
END

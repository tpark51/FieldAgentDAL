ALTER PROCEDURE PensionList(
    @AgencyId AS int
)
AS
BEGIN
    SELECT
    ay.LongName as 'AgencyName',
    aa.BadgeId,
    a.LastName + ', ' + a.FirstName as 'NameLastFirst',
    a.DateOfBirth,
    aa.DeactivationDate
    FROM Agent a
    INNER join AgencyAgent aa on a.AgentId = aa.AgentId
    INNER JOIN SecurityClearance sc on aa.SecurityClearanceId = sc.SecurityClearanceId
    INNER JOIN Agency ay on aa.AgencyId = ay.AgencyId
    WHERE sc.SecurityClearanceName = 'Retired'
    and ay.AgencyId = @AgencyId;
END

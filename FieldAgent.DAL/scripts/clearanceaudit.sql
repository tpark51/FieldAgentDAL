ALTER PROCEDURE ClearanceAudit(
    @AgencyId AS int,
    @SecurityClearanceId AS int
)
AS
BEGIN
    SELECT
    aa.BadgeId,
    a.LastName + ', ' + a.FirstName as 'NameLastFirst',
    a.DateOfBirth,
    aa.ActivationDate,
    aa.DeactivationDate
    FROM Agent a
    INNER join AgencyAgent aa on a.AgentId = aa.AgentId
    INNER JOIN SecurityClearance sc on aa.SecurityClearanceId = sc.SecurityClearanceId
    INNER JOIN Agency ay on aa.AgencyId = ay.AgencyId
    WHERE sc.SecurityClearanceId = @SecurityClearanceId
    and ay.AgencyId = @AgencyId;
END

namespace SimpleToDoApp.Helpers.DTOs.Requests;

public record EmailParams
(
    string senderEmail, string emailSubject, string emailBody,
    string recipientEmail, string senderName, bool isHtml
);
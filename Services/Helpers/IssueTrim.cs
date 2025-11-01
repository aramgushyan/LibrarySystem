using Applications.Dto.Issue;

namespace Services.Helpers;

public static class IssueTrim
{
    public static void TrimAddIssueDto(AddIssueDto addIssueDto)
    {
        addIssueDto.ReaderCardNumber = addIssueDto.ReaderCardNumber.Trim();
        addIssueDto.LibraryCode = addIssueDto.LibraryCode.Trim();
    }

    public static void TrimUpdateIssueDto(UpdateIssueDto updateIssueDto)
    {
        updateIssueDto.ReaderCardNumber = updateIssueDto.ReaderCardNumber.Trim();
        updateIssueDto.LibraryCode = updateIssueDto.LibraryCode.Trim();
    }

}
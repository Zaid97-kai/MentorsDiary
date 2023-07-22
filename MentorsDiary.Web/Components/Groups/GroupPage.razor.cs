using AntDesign;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using HttpService.Services;
using MentorsDiary.Application.Bases.Enums;
using MentorsDiary.Application.Bases.Interfaces.IHaves;
using MentorsDiary.Application.Entities.Bases.Filters;
using MentorsDiary.Application.Entities.GroupEvents.Domains;
using MentorsDiary.Application.Entities.Students.Domains;
using MentorsDiary.Application.Entities.Users.Domains;
using MentorsDiary.Web.Data.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using OfficeOpenXml;
using Group = MentorsDiary.Application.Entities.Groups.Domains.Group;

namespace MentorsDiary.Web.Components.Groups;

/// <summary>
/// Class GroupPage.
/// Implements the <see cref="ComponentBase" />
/// </summary>
/// <seealso cref="ComponentBase" />
public partial class GroupPage
{
    /// <summary>
    /// Gets or sets the group identifier.
    /// </summary>
    /// <value>The group identifier.</value>
    [Parameter]
    public int GroupId { get; set; }

    /// <summary>
    /// Gets or sets the group event service.
    /// </summary>
    /// <value>The group event service.</value>
    [Inject]
    private GroupEventService GroupEventService { get; set; } = null!;

    /// <summary>
    /// Gets or sets the js.
    /// </summary>
    /// <value>The js.</value>
    [Inject]
    private IJSRuntime Js { get; set; } = null!;

    /// <summary>
    /// Gets or sets the event service.
    /// </summary>
    /// <value>The event service.</value>
    [Inject]
    private EventService EventService { get; set; } = null!;

    /// <summary>
    /// Gets or sets the authentication service.
    /// </summary>
    /// <value>The authentication service.</value>
    [Inject]
    private AuthenticationService AuthenticationService { get; set; } = null!;

    /// <summary>
    /// Gets or sets the student service.
    /// </summary>
    /// <value>The student service.</value>
    [Inject]
    private StudentService StudentService { get; set; } = null!;

    /// <summary>
    /// Gets or sets the group service.
    /// </summary>
    /// <value>The group service.</value>
    [Inject]
    private GroupService GroupService { get; set; } = null!;

    /// <summary>
    /// Gets or sets the navigation manager.
    /// </summary>
    /// <value>The navigation manager.</value>
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;


    /// <summary>
    /// Gets or sets the message service.
    /// </summary>
    /// <value>The message service.</value>
    [Inject]
    private MessageService MessageService { get; set; } = null!;

    /// <summary>
    /// Gets the current group.
    /// </summary>
    /// <value>The current group.</value>
    private Group CurrentGroup { get; set; }

    /// <summary>
    /// Gets or sets the current user.
    /// </summary>
    /// <value>The current user.</value>
    private User CurrentUser => (User)AuthenticationService?.AuthorizedUser!;

    /// <summary>
    /// The users
    /// </summary>
    /// <value>The students.</value>
    private List<Student>? Students { get; set; } = new();

    /// <summary>
    /// The group events
    /// </summary>
    /// <value>The group events.</value>
    private List<GroupEvent>? GroupEvents { get; set; } = new();

    /// <summary>
    /// The is loading
    /// </summary>
    private bool _isLoading;

    /// <summary>
    /// Initializes a new instance of the <see cref="GroupPage"/> class.
    /// </summary>
    /// <param name="currentGroup">The current group.</param>
    public GroupPage(Group currentGroup)
    {
        CurrentGroup = currentGroup;
    }

    /// <summary>
    /// Create student as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async void CreateStudentAsync()
    {
        _isLoading = true;

        StateHasChanged();
        var response = await StudentService.CreateAsync(new Student
        {
            Name = "Имя", 
            GroupId = GroupId
        });

        if (response.IsSuccessStatusCode)
            NavigationManager.NavigateTo($"student/{JsonConvert.DeserializeObject<Student>(await response.Content.ReadAsStringAsync())!.Id}/{GroupId}");
        else
            await MessageService.Error(response.ReasonPhrase);

        _isLoading = false;
    }

    /// <summary>
    /// Shows the modal add event window.
    /// </summary>
    public void ShowModalAddEventWindow()
    {
        NavigationManager.NavigateTo("/group-event/create");
    }

    /// <summary>
    /// On initialized as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    protected override async Task OnInitializedAsync()
    {
        CurrentGroup = await GroupService.GetIdAsync(GroupId);

        await GetListAsync();
    }

    /// <summary>
    /// Get list as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    private async Task GetListAsync()
    {
        _isLoading = true;
        StateHasChanged();

        var filterParams = new FilterParams()
        {
            FilterOption = EnumFilterOptions.Contains,
            ColumnName = nameof(GroupId),
            FilterValue = Convert.ToString(GroupId)
        };

        var responseMessageStudent = await StudentService.GetAllByFilterAsync(filterParams);
        if (responseMessageStudent != null)
            Students = JsonConvert.DeserializeObject<List<Student>>(await responseMessageStudent.Content.ReadAsStringAsync());

        var responseMessageGroupEvent = await GroupEventService.GetAllByFilterAsync(filterParams);
        if (responseMessageGroupEvent != null)
            GroupEvents = JsonConvert.DeserializeObject<List<GroupEvent>>(await responseMessageGroupEvent.Content.ReadAsStringAsync());

        _isLoading = false;
        StateHasChanged();
    }

    /// <summary>
    /// Exports the groups.
    /// </summary>
    /// <returns>System.Byte[].</returns>
    private byte[] ExportGroups()
    {
        var stream = new MemoryStream();

        using (var package = new ExcelPackage(stream))
        {
            var workSheet = package.Workbook.Worksheets.Add(CurrentGroup?.Name);
            workSheet.Cells[1, 1].Value = "ФИО";
            workSheet.Cells[1, 2].Value = "Дата рождения";
            workSheet.Cells[1, 3].Value = "Почта";
            workSheet.Cells[1, 4].Value = "Телефон";
            workSheet.Cells[1, 5].Value = "Адрес";
            workSheet.Cells[1, 6].Value = "Группа";

            workSheet.Cells["A1:H1"].Style.Font.Bold = true;

            var startRowId = 2;
            foreach (var student in Students!)
            {
                workSheet.Cells[startRowId, 1].Value = student.Name;
                workSheet.Cells[startRowId, 2].Value = student.BirthDate != null ? student.BirthDate.Value.ToString("d") : "";
                workSheet.Cells[startRowId, 3].Value = student.Email;
                workSheet.Cells[startRowId, 4].Value = student.Phone;
                workSheet.Cells[startRowId, 5].Value = student.Address;
                workSheet.Cells[startRowId, 6].Value = student.Group.Name;
                startRowId++;
            }

            workSheet.Cells["A1:H100"].AutoFitColumns();

            return package.GetAsByteArray();
        }
    }

    /// <summary>
    /// Downloads the excel file.
    /// </summary>
    public void DownloadExcelFile()
    {
        var excelBytes = ExportGroups();
        Js.InvokeVoidAsync("saveAsFile", $"List_of_{CurrentGroup!.Name}_{DateTime.Now.ToString("d")}.xlsx", Convert.ToBase64String(excelBytes));
    }

    /// <summary>
    /// Loads the files.
    /// </summary>
    /// <param name="e">The <see cref="InputFileChangeEventArgs" /> instance containing the event data.</param>
    private async Task LoadFiles(InputFileChangeEventArgs e)
    {
        using (var memoryStream = new MemoryStream())
        {
            await e.File.OpenReadStream(e.File.Size).CopyToAsync(memoryStream);
            using (var workBook = new XLWorkbook(memoryStream))
            {
                foreach (var worksheet in workBook.Worksheets)
                {
                    foreach (var row in worksheet.RowsUsed().Skip(1))
                    {
                        try
                        {
                            var name = row.Cell(1).Value.ToString();
                            var birthDate = row.Cell(2).Value.IsDateTime ? row.Cell(2).Value.GetDateTime() : DateTime.Now;
                            var email = row.Cell(3).Value.ToString();
                            var phone = row.Cell(4).Value.ToString();
                            var address = row.Cell(5).Value.ToString();

                            var student = new Student()
                            {
                                Name = name,
                                BirthDate = birthDate == DateTime.Now ? null : birthDate,
                                Email = email,
                                Phone = phone,
                                Address = address,
                                GroupId = CurrentGroup!.Id
                            };
                            if (student != null)
                            {
                                await StudentService.CreateAsync(student);
                            }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
        }

        await OnInitializedAsync();
    }

    /// <summary>
    /// Remove student as an asynchronous operation.
    /// </summary>
    /// <param name="student">The student.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    private async Task RemoveStudentAsync(Student student)
    {
        _isLoading = true;
        StateHasChanged();

        var response = await StudentService.DeleteAsync(student.Id);
        if (response.IsSuccessStatusCode)
            await MessageService.Success($"Студент {student.Name} успешно удален.");
        else
            await MessageService.Error(response.ReasonPhrase);

        await GetListAsync();
    }

    /// <summary>
    /// Update student as an asynchronous operation.
    /// </summary>
    /// <param name="student">The student.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    private async Task UpdateStudentAsync(IHaveId student)
    {
        NavigationManager.NavigateTo($"student/{student.Id}/{GroupId}");
    }

    /// <summary>
    /// Shows the student page asynchronous.
    /// </summary>
    /// <param name="student">The student.</param>
    /// <returns>Task.</returns>
    /// <exception cref="System.NotImplementedException"></exception>
    private Task ShowStudentPageAsync(Student student)
    {
        throw new NotImplementedException();
    }
}
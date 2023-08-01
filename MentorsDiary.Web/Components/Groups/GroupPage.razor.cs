using AntDesign;
using ClosedXML.Excel;
using HttpService.Services;
using MentorsDiary.Application.Bases.Enums;
using MentorsDiary.Application.Bases.Interfaces.IHaves;
using MentorsDiary.Application.Entities.Bases.Filters;
using MentorsDiary.Application.Entities.GroupEvents.Domains;
using MentorsDiary.Application.Entities.Parents.Domains;
using MentorsDiary.Application.Entities.ParentStudents.Domains;
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
    /// Gets or sets the parent service.
    /// </summary>
    /// <value>The parent service.</value>
    [Inject]
    private ParentService ParentService { get; set; } = null!;

    /// <summary>
    /// Gets or sets the parent student service.
    /// </summary>
    /// <value>The parent student service.</value>
    [Inject]
    private ParentStudentService ParentStudentService { get; set; } = null!;

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
    private Group CurrentGroup { get; set; } = new();

    /// <summary>
    /// Gets or sets the current user.
    /// </summary>
    /// <value>The current user.</value>
    private User CurrentUser => (User)AuthenticationService.AuthorizedUser!;

    /// <summary>
    /// The users
    /// </summary>
    /// <value>The students.</value>
    private List<Student> Students { get; set; } = new();

    /// <summary>
    /// The group events
    /// </summary>
    /// <value>The group events.</value>
    private List<GroupEvent> GroupEvents { get; set; } = new();

    /// <summary>
    /// The student data visible
    /// </summary>
    private bool _studentDataVisible;

    /// <summary>
    /// The is loading
    /// </summary>
    private bool _isLoading;

    /// <summary>
    /// The selected student
    /// </summary>
    private Student? _selectedStudent = new();

    /// <summary>
    /// Gets the base URI.
    /// </summary>
    /// <value>The base URI.</value>
    private string BaseUri => $"https://localhost:7225/group-page/{GroupId}/#close-block";

    /// <summary>
    /// Create student as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async void CreateStudentAsync()
    {
        _isLoading = true;
        StateHasChanged();

        var responseCreateStudent = await StudentService.CreateAsync(new Student
        {
            Name = "Имя",
            GroupId = GroupId,
            DateCreated = DateTime.Now,
            UserCreated = CurrentUser.Name
        });

        var createdStudent = JsonConvert.DeserializeObject<Student>(await responseCreateStudent.Content.ReadAsStringAsync());

        var mother = new Parent();
        var father = new Parent();

        var responseCreateParentMother = await ParentService.CreateAsync(mother);
        var responseCreateParentFather = await ParentService.CreateAsync(father);

        var createdParentMother = JsonConvert.DeserializeObject<Parent>(await responseCreateParentMother.Content.ReadAsStringAsync());
        var createdParentFather = JsonConvert.DeserializeObject<Parent>(await responseCreateParentFather.Content.ReadAsStringAsync());

        await ParentStudentService.CreateAsync(new ParentStudent { ParentId = createdParentMother.Id, StudentId = createdStudent.Id });
        await ParentStudentService.CreateAsync(new ParentStudent { ParentId = createdParentFather.Id, StudentId = createdStudent.Id });

        if (responseCreateStudent.IsSuccessStatusCode)
            NavigationManager.NavigateTo($"student/{createdStudent.Id}/{GroupId}");
        else
            await MessageService.Error(responseCreateStudent.ReasonPhrase);

        _isLoading = false;
    }

    /// <summary>
    /// Create group event as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async void CreateGroupEventAsync()
    {
        _isLoading = true;
        StateHasChanged();

        var response = await GroupEventService.CreateAsync(new GroupEvent
        {
            GroupId = GroupId,
            DateCreated = DateTime.Now,
            UserCreated = CurrentUser.Name
        });

        if (response.IsSuccessStatusCode)
            NavigationManager.NavigateTo($"group-event/{JsonConvert.DeserializeObject<GroupEvent>(await response.Content.ReadAsStringAsync())!.Id}/{GroupId}");
        else
            await MessageService.Error(response.ReasonPhrase);

        _isLoading = false;
    }

    /// <summary>
    /// On initialized as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    protected override async Task OnInitializedAsync()
    {
        CurrentGroup = (await GroupService.GetIdAsync(GroupId))!;

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
            Students = JsonConvert.DeserializeObject<List<Student>>(await responseMessageStudent.Content.ReadAsStringAsync()) ?? new List<Student>();

        var responseMessageGroupEvent = await GroupEventService.GetAllByFilterAsync(filterParams);
        if (responseMessageGroupEvent != null)
            GroupEvents = JsonConvert.DeserializeObject<List<GroupEvent>>(await responseMessageGroupEvent.Content.ReadAsStringAsync()) ?? new List<GroupEvent>();

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
    /// Updates the student asynchronous.
    /// </summary>
    /// <param name="student">The student.</param>
    private void UpdateStudentAsync(IHaveId student)
    {
        NavigationManager.NavigateTo($"student/{student.Id}/{GroupId}");
    }

    /// <summary>
    /// Shows the student page asynchronous.
    /// </summary>
    /// <param name="student">The student.</param>
    private void ShowStudentPageAsync(Student student)
    {
        _selectedStudent = student;
        NavigationManager.NavigateTo($"https://localhost:7225/group-page/{GroupId}/#modal-block", true);
    }

    /// <summary>
    /// Remove group event as an asynchronous operation.
    /// </summary>
    /// <param name="groupEvent">The group event.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    private async Task RemoveGroupEventAsync(GroupEvent groupEvent)
    {
        _isLoading = true;
        StateHasChanged();

        var response = await GroupEventService.DeleteAsync(groupEvent.Id);
        if (response.IsSuccessStatusCode)
            await MessageService.Success($"Событие {groupEvent.Name} успешно удалено.");
        else
            await MessageService.Error(response.ReasonPhrase);

        await GetListAsync();
    }

    /// <summary>
    /// Updates the group event asynchronous.
    /// </summary>
    /// <param name="groupEvent">The group event.</param>
    private void UpdateGroupEventAsync(IHaveId groupEvent)
    {
        NavigationManager.NavigateTo($"group-event/{groupEvent.Id}/{GroupId}");
    }

    /// <summary>
    /// Shows the group event page asynchronous.
    /// </summary>
    /// <param name="groupEvent">The group event.</param>
    /// <returns>Task.</returns>
    /// <exception cref="System.NotImplementedException"></exception>
    private Task ShowGroupEventPageAsync(GroupEvent groupEvent)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Method invoked after each time the component has been rendered.
    /// </summary>
    /// <param name="firstRender">Set to <c>true</c> if this is the first time <see cref="M:Microsoft.AspNetCore.Components.ComponentBase.OnAfterRender(System.Boolean)" /> has been invoked
    /// on this component instance; otherwise <c>false</c>.</param>
    /// <remarks>The <see cref="M:Microsoft.AspNetCore.Components.ComponentBase.OnAfterRender(System.Boolean)" /> and <see cref="M:Microsoft.AspNetCore.Components.ComponentBase.OnAfterRenderAsync(System.Boolean)" /> lifecycle methods
    /// are useful for performing interop, or interacting with values received from <c>@ref</c>.
    /// Use the <paramref name="firstRender" /> parameter to ensure that initialization work is only performed
    /// once.</remarks>
    protected override void OnAfterRender(bool firstRender)
    {
        if(firstRender)
            NavigationManager.NavigateTo(BaseUri);

        base.OnAfterRender(firstRender);
    }
}
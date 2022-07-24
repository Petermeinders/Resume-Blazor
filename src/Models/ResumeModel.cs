
public class ResumeModel
{
    public ExperienceModel[] ExperienceModel { get; set; } = new ExperienceModel[0];
    public ContactModel ContactModel { get; set; } = new ContactModel();
    public EducationModel EducationModel { get; set; }
    public SkillsModel SkillsModel { get; set; }
    public PersonalProjectsModel[] PersonalProjectsModel { get; set; }
}

public class ContactModel
{
    public string Email { get; set; }
    public string Location { get; set; }
    public string Phone { get; set; }
    public string website { get; set; }

    public string GithubLink { get; set; }
    public string LinkedInProfile { get; set; }
}

public class EducationModel
{
    public string SchoolName { get; set; }
    public string Degree { get; set; }
    public string GraduationDate { get; set; }
    public string Location { get; set; }
}

public class SkillsModel
{
    public string[] Tools { get; set; } = new string[0];
    public string[] Skills { get; set; } = new string[0];
}

public class ExperienceModel
{
    public string Header { get; set; } = "HEADER";
    public string SubHeader { get; set; }
    public string CompanyName { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public string[] BulletPoints { get; set; }
}

public class PersonalProjectsModel
{
    public string ProjectName { get; set; }
    public string ProjectDescription { get; set; }
    public string ProjectLink { get; set; }
}

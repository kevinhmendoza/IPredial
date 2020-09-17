export class About {
    public Title: string;
    public Description: string;
    public Others: string;
}

export class SocialNetwork {
    public Name: string;
    public FaIcon: string;
    public ProfileUrl: string;
}

export class Language {
    public Language: string;
    public Description: string;
}

export class Dates {
    public Start: string;
    public End: string;
}

export class Education {
    public Title: string;
    public Entity: string;
    public Dates: Dates;
}

export class Dates2 {
    public Start: string;
    public End: string;
}

export class Experience {
    public Name: string;
    public JobTitle: string;
    public Description: string;
    public Dates: Dates2;
}

export class Skill {
    public Name: string;
    public Percentage: number;
}

export class OtherSkill {
    public Name: string;
    public Percentage: number;
}

export class Summary {
    public ShortName: string;
    public Name: string;
    public Phone: string;
    public Email: string;
    public About: About;
    public Age:number;
    public City:string;
    public Country:string;
    public SocialNetworks: SocialNetwork[];
    public Languages: Language[];
    public Education: Education[];
    public BirthdayDate:string;
    public Experiences: Experience[];
    public Achievements: string;
    public Skills: Skill[];
    public OtherSkills: OtherSkill[];
    public HideCopyright: boolean;
}
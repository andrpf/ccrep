namespace CcRep.Components.UserAuditing
{
    interface IUserAuditing
    {
        string UserCreatedId { get; set; }
        string UserLastEditedId { get; set; }
    }
}

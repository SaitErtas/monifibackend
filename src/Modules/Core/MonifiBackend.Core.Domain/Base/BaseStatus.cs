﻿namespace MonifiBackend.Core.Domain.Base;

public enum BaseStatus
{
    Active = 1,
    Passive = 2,
    Deleted = -1,
}
public class BaseStatusStrings
{
    public const string ACTIVE = "Active";
    public const string PASSIVE = "Passive";
    public const string DELETED = "Deleted";
}

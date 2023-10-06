﻿using System.ComponentModel.DataAnnotations;

namespace MyMusicWebAPI.InDto;

public class AccountVerificationInDto
{
    [Required]
    public string Email { get; set; } = null!;
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace practice1.Models;

public partial class Employee
{



[DisplayName("Emp_Id")]
    [Required]
public int EmpId { get; set; }

 [Required]
 [DisplayName("FirstName")]
public string? FirstName { get; set; }

    [Required]
    [DisplayName("LastName")]
public string? LastName { get; set; }

    [Required]
    [Length(10,13)]
    [DisplayName("PhoneNo.")]
public string? Phone { get; set; }

    [Required]
    [DisplayName("Department")]
public string? Department { get; set; }
}

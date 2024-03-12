﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.ValueObjects;

public struct Document
{
    public int Title { get; set; }
    public string Number { get; set; }
    public DateOnly Date { get; set; }
    public string Mentions { get; set; }
    public int Copies { get; set; }
}
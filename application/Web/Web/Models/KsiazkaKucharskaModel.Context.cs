﻿

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace Web.Models
{

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class KsiazkaKucharskaModelContainer : DbContext
{
    public KsiazkaKucharskaModelContainer()
        : base("name=KsiazkaKucharskaModelContainer")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<Przepis> PrzepisSet { get; set; }

    public virtual DbSet<Uzytkownik> UzytkownikSet { get; set; }

    public virtual DbSet<PasswordReminder> PasswordReminderSet { get; set; }

}

}


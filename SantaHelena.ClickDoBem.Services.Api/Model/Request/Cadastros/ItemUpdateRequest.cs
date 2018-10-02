﻿using System;
using System.ComponentModel.DataAnnotations;

namespace SantaHelena.ClickDoBem.Services.Api.Model.Request.Cadastros
{

    /// <summary>
    /// Request de INSERT de item
    /// </summary>
    public class ItemUpdateRequest : ItemInsertRequest
    {

        /// <summary>
        /// Id do registro
        /// </summary>
        [Key]
        public Guid Id { get; set; }

    }

}

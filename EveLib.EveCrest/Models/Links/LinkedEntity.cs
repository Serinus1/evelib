﻿// ***********************************************************************
// Assembly         : EveLib.EveCrest
// Author           : Lars Kristian
// Created          : 12-16-2014
//
// Last Modified By : Lars Kristian
// Last Modified On : 12-17-2014
// ***********************************************************************
// <copyright file="LinkedEntity.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Linq;
using System.Runtime.Serialization;

namespace eZet.EveLib.EveCrestModule.Models.Links {
    /// <summary>
    ///     Class LinkedEntity. A base class for linked resources that has a name and ID.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DataContract]
    public class LinkedEntity<T> : ILinkedEntity<T> {
        private int _id;

        /// <summary>
        ///     Gets or sets the identifier. If the ID is not returned explicitly by the API, it will try to infer the ID from the
        ///     Href. See IsExplicitId. If the ID is negative, it failed to infer the ID.
        /// </summary>
        /// <value>The identifier.</value>
        [DataMember(Name = "id")]
        public int Id {
            get {
                if (!IsExplicitId)
                    inferId();
                return _id;
            }
            set {
                _id = value;
                IsExplicitId = true;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance has an explicit identifier.
        /// </summary>
        /// <value><c>true</c> if this instance has explicit identifier; otherwise, <c>false</c>.</value>
        public bool IsExplicitId { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        ///     The entity href
        /// </summary>
        /// <value>The href.</value>
        [DataMember(Name = "href")]
        public Href<T> Href { get; set; }

        private int inferId() {
            int id;
            string[] href = Href.Uri.Split(new[] {'/'}, StringSplitOptions.RemoveEmptyEntries);
            if (!int.TryParse(href.Last(), out id))
                id = -1;
            _id = id;
            return id;
        }
    }
}
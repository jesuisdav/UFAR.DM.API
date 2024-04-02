﻿namespace UFAR.DM.API.Data.Entities {
    public record WordEntity : BaseEntity {
        public string? Word { get; set; }
        required public int SectionId { get; set; }
        required public SectionEntity Section { get; set; }
    }
}

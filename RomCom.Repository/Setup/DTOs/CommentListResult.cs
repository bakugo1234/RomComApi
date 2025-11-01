using System.Collections.Generic;
using RomCom.Model.DTOs.Comment.Responses;

namespace RomCom.Repository.Setup.DTOs
{
    public class CommentListResult
    {
        public required List<CommentDto> Comments { get; set; }
        public int TotalCount { get; set; }
    }
}

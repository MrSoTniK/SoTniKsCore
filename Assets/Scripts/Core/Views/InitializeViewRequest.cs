using Core.Infrastructure.Components.Requests;
using System;

namespace Core.Views 
{
    [Serializable]
    public struct InitializeViewRequest : IRequest
    {
        public ViewBase View;
    }
}
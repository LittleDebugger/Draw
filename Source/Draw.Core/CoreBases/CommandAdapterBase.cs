using System;
using Draw.Core.CoreInterfaces;
using Microsoft.Extensions.Logging;

namespace Draw.Core.CoreBases
{
    public abstract class CommandAdapterBase<TCoreCommand, TInputContext, TPixelData>
        where TPixelData : IPixelData
    {
        protected readonly TCoreCommand CoreCommand;
        protected readonly ILogger Logger;

        protected CommandAdapterBase(TCoreCommand coreCommand, ILogger logger)
        {
            CoreCommand = coreCommand;
            Logger = logger;
        }

        public ICanvas<TPixelData> Execute(ICanvas<TPixelData> canvas, TInputContext inputContext)
        {
            if (inputContext == null)
            {
                throw new ArgumentNullException(nameof(inputContext));
            }

            Logger.LogDebug($"{GetType()}: Received InputContext: {inputContext}");

            InitialValidation(canvas, inputContext);
            return ExecuteInternal(canvas, inputContext);
        }

        protected abstract void InitialValidation(ICanvas<TPixelData> canvas, TInputContext inputContext);

        protected abstract ICanvas<TPixelData> ExecuteInternal(ICanvas<TPixelData> canvas, TInputContext input);
    }
}
using System;
using System.Collections.Generic;

namespace FastColoredTextBoxNS
{
    public class CommandManager
    {
        private const int maxHistoryLength = 200;
        private readonly LimitedStack<UndoableCommand> history;
        private readonly Stack<UndoableCommand> redoStack = new Stack<UndoableCommand>();
        private int _disabledCommands;
        private int autoUndoCommands;

        public CommandManager(TextSource ts)
        {
            history = new LimitedStack<UndoableCommand>(maxHistoryLength);
            TextSource = ts;
        }

        public TextSource TextSource { get; private set; }

        public bool UndoEnabled
        {
            get { return history.Count > 0; }
        }

        public bool RedoEnabled
        {
            get { return redoStack.Count > 0; }
        }

        public void ExecuteCommand(Command cmd)
        {
            if (_disabledCommands > 0)
                return;

            //multirange ?
            if (cmd.ts.CurrentTb.Selection.ColumnSelectionMode)
                if (cmd is UndoableCommand)
                    //make wrapper
                    cmd = new MultiRangeCommand((UndoableCommand) cmd);

            if (cmd is UndoableCommand)
            {
                //if range is ColumnRange, then create wrapper
                (cmd as UndoableCommand).autoUndo = autoUndoCommands > 0;
                history.Push(cmd as UndoableCommand);
            }

            try
            {
                cmd.Execute();
            }
            catch (ArgumentOutOfRangeException)
            {
                //OnTextChanging cancels enter of the text
                if (cmd is UndoableCommand)
                    history.Pop();
            }
            //
            redoStack.Clear();
            //
            TextSource.CurrentTb.OnUndoRedoStateChanged();
        }

        public void Undo()
        {
            if (history.Count > 0)
            {
                var cmd = history.Pop();
                //
                BeginDisableCommands(); //prevent text changing into handlers
                try
                {
                    cmd.Undo();
                }
                finally
                {
                    EndDisableCommands();
                }
                //
                redoStack.Push(cmd);
            }

            //undo next autoUndo command
            if (history.Count > 0)
            {
                if (history.Peek().autoUndo)
                    Undo();
            }

            TextSource.CurrentTb.OnUndoRedoStateChanged();
        }

        private void EndDisableCommands()
        {
            _disabledCommands--;
        }

        private void BeginDisableCommands()
        {
            _disabledCommands++;
        }

        public void EndAutoUndoCommands()
        {
            autoUndoCommands--;
            if (autoUndoCommands == 0)
                if (history.Count > 0)
                    history.Peek().autoUndo = false;
        }

        public void BeginAutoUndoCommands()
        {
            autoUndoCommands++;
        }

        internal void ClearHistory()
        {
            history.Clear();
            redoStack.Clear();
            TextSource.CurrentTb.OnUndoRedoStateChanged();
        }

        internal void Redo()
        {
            if (redoStack.Count == 0)
                return;
            UndoableCommand cmd;
            BeginDisableCommands(); //prevent text changing into handlers
            try
            {
                cmd = redoStack.Pop();
                if (TextSource.CurrentTb.Selection.ColumnSelectionMode)
                    TextSource.CurrentTb.Selection.ColumnSelectionMode = false;
                TextSource.CurrentTb.Selection.Start = cmd.sel.Start;
                TextSource.CurrentTb.Selection.End = cmd.sel.End;
                cmd.Execute();
                history.Push(cmd);
            }
            finally
            {
                EndDisableCommands();
            }

            //redo command after autoUndoable command
            if (cmd.autoUndo)
                Redo();

            TextSource.CurrentTb.OnUndoRedoStateChanged();
        }
    }

    public abstract class Command
    {
        internal TextSource ts;

        public abstract void Execute();
    }

    internal class RangeInfo
    {
        public RangeInfo(Range r)
        {
            Start = r.Start;
            End = r.End;
        }

        public Place Start { get; set; }

        public Place End { get; set; }

        internal int FromX
        {
            get
            {
                if (End.iLine < Start.iLine) return End.iChar;
                if (End.iLine > Start.iLine) return Start.iChar;
                return Math.Min(End.iChar, Start.iChar);
            }
        }
    }

    public abstract class UndoableCommand : Command
    {
        internal bool autoUndo;
        internal RangeInfo lastSel;
        internal RangeInfo sel;

        protected UndoableCommand(TextSource ts)
        {
            this.ts = ts;
            sel = new RangeInfo(ts.CurrentTb.Selection);
        }

        public virtual void Undo()
        {
            OnTextChanged(true);
        }

        public override void Execute()
        {
            lastSel = new RangeInfo(ts.CurrentTb.Selection);
            OnTextChanged(false);
        }

        protected virtual void OnTextChanged(bool invert)
        {
            var b = sel.Start.iLine < lastSel.Start.iLine;
            if (invert)
                ts.OnTextChanged(sel.Start.iLine, b ? sel.Start.iLine : lastSel.Start.iLine);
            else
                ts.OnTextChanged(b ? sel.Start.iLine : lastSel.Start.iLine, lastSel.Start.iLine);
        }

        public abstract UndoableCommand Clone();
    }
}
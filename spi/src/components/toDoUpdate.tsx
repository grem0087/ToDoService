import * as React from 'react';
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux';

import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';

import Dialog from '@material-ui/core/Dialog';
import DialogContent from '@material-ui/core/DialogContent';
import { createToDoRequest, switchUpdateWindow } from '../actions';
import ToDoModel from '../types';

export type TodoUpdateType = {
  open: boolean;
  title: string;
  description: string;
  estimate: number;
  addTodo: (dto: ToDoModel) => void;
  switchWindow: (open: boolean) => void;
};

class TodoUpdate extends React.Component<TodoUpdateType> {
  tableId: string = "1";
  dto: ToDoModel;

  constructor(props) {
    super(props)
    this.dto = {} as ToDoModel;
    this.handleAddTodo = this.handleAddTodo.bind(this)
  }
  render() {
    const buttonStyle = {
      margin: '10px',
      flexGrow: 1,
    };

    return (
      <div>
        <Dialog onClose={() => this.props.switchWindow(false)} open={this.props.open} aria-labelledby="form-dialog-title">
          <DialogContent>
            <form>
              <div>
                <TextField onChange={e => this.dto.title = e.target.value} label="Title" id="title" />
              </div>
              <div>
                <TextField onChange={e => this.dto.estimation = Number.parseInt(e.target.value)} type="number" id="estimate" label="Estimation" />
              </div>
              <div>
                <TextField onChange={e => this.dto.description = e.target.value} id="description" multiline rows={4} label="Description" />
              </div>

              <br />
              <div >
                <Button style={buttonStyle} onClick={() => this.props.switchWindow(false)} variant="contained" color="secondary">
                  Cancel
                    </Button>
                <Button style={buttonStyle} onClick={this.handleAddTodo} variant="contained" color="primary">
                  Add
                    </Button>
              </div>
            </form>
          </DialogContent>
        </Dialog>
      </div>
    )
  };

  handleAddTodo() {
    this.dto.tableId = this.tableId;
    this.props.addTodo(this.dto);
    this.props.switchWindow(false);
  }
}

const mapStateToProps = (state, ownProps) => {
  return {
    open: state.crudForm.open,
    ...state
  }
};

const mapDispatchToProps = dispatch => ({
  addTodo: bindActionCreators(createToDoRequest, dispatch),
  switchWindow: bindActionCreators(switchUpdateWindow, dispatch)
});

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(TodoUpdate);
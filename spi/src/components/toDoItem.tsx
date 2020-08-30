import React, { Component } from 'react';
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux';

import Paper from '@material-ui/core/Paper';
import CloseIcon from '@material-ui/icons/Close';
import IconButton from '@material-ui/core/IconButton';
import { deleteToDoRequest } from '../actions';

type ToDoItemProps = {
    id: number;
    name: string;
    estimation?: number;
    deleteToDoItem: (id:number) => void
};

export class ToDoItem extends Component<ToDoItemProps> {
    render() {
        const { name, estimation, id, deleteToDoItem } = this.props;
        return (
            <div style={{ margin: '10px' }}>
                <IconButton edge="start" color="secondary" style={{ float: 'right' }} onClick={()=>deleteToDoItem(id)}>
                    <CloseIcon color="secondary" />
                </IconButton>
                <Paper style={{ padding: '25px' }} >
                    <span>{name}</span> <span>{estimation}</span>
                </Paper>
            </div>
        )
    };
}

const mapDispatchToProps = dispatch => ({
    deleteToDoItem: bindActionCreators(deleteToDoRequest, dispatch)
   });
  
  export default connect(
    null,
    mapDispatchToProps
  )(ToDoItem);
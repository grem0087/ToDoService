import React, { Component } from 'react';
import Box from '@material-ui/core/Box';

import { connect } from 'react-redux'

import ToDoItem from './toDoItem';
import ToDoModel from '../types';

  type TodoViewType = {
    todoItems: ToDoModel[];
  };

export class ToDoList extends Component<TodoViewType> {
  constructor(props) {
    super(props);
    
  }
    render()
    {
        const {todoItems} = this.props;
        const listStyle = {
          padding: '15px'
        };
        return( 
            <div>
              <Box bgcolor="info.main" style={listStyle}>
                { todoItems.map((todo, index) => {
                    return <ToDoItem id={todo.id} name={todo.title} estimation={todo.estimation} key={index} />
                })}
              </Box>
            </div>
    )};
  }

  const mapStateToProps = ( state, ownProps ) => {
    return {
      todoItems: state.crudForm.toDoItems,
      ...state
    }};
      
    export default connect(
      mapStateToProps,
      {}
    )(ToDoList)
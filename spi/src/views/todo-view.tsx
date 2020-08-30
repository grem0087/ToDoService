import * as React from 'react';
import Grid from '@material-ui/core/Grid';

import ToDoList from '../components/toDoList';
import  ToDoUpdate  from '../components/toDoUpdate';
import SignInMenu from '../components/signInMenu';
import TopMenuBar from '../components/topMenuBar';

class TodoView extends React.Component {
  render() 
  {
      return (
        <div>
          <TopMenuBar />
          <Grid container spacing={5}>
            <Grid container item xs={12} spacing={3}>
            <Grid item xs={4}>
              <ToDoList />
              </Grid>
              <Grid item xs={4}>
              <ToDoUpdate />
              <SignInMenu />
              </Grid>
            </Grid>
          </Grid>
        </div>
  )};

      }
export default TodoView;
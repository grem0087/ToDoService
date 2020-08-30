import React from 'react';
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux';

import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import Button from '@material-ui/core/Button';
import IconButton from '@material-ui/core/IconButton';
import MenuIcon from '@material-ui/icons/Menu';
import AutorenewIcon from '@material-ui/icons/Autorenew';
import LinearProgress from '@material-ui/core/LinearProgress';
import Fade from '@material-ui/core/Fade';

import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';

import { switchUpdateWindow, getAllTodosRequest, switchSignInWindow } from '../actions';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      flexGrow: 1,
    },
    menuButton: {
      marginRight: theme.spacing(2),
    },

    title: {
        paddingLeft:'20px',
        textAlign: 'left',
        flexGrow: 1,
    },
  }),
);
export function ButtonAppBar(props) {
  let strData = sessionStorage.getItem('userData');
  if(strData != null)
  {
    let data = JSON.parse(strData);
    console.log(data['profileObj'].email)
  }
  
  
    const classes = useStyles();
    return (
        <div className={classes.root}>
            <AppBar position="static">
                <Toolbar>
                    <IconButton edge="start" color="inherit" aria-label="menu">
                         <MenuIcon /> 
                    </IconButton>
                    <IconButton edge="start" color="inherit" aria-label="menu" onClick={()=>props.getAllTodos('prj')}>
                         <AutorenewIcon />
                    </IconButton>
                    <Button variant="contained" color="secondary" onClick={()=>props.switchWindow(!props.open)}>Create Issue</Button>
                      <Typography className={classes.title} variant="h6" color="inherit">
                        Project name
                      </Typography>
                    <Button color="inherit" onClick={()=>props.switchSignInClick(!props.isSignInOpen)}>Login</Button>
                </Toolbar>
            </AppBar>
            <Fade in={props.isLoading}>
            <LinearProgress color="secondary"  />
            </Fade>
        </div>
    );
}

const mapStateToProps = ( state, ownProps ) => {
    return {
      isSignInOpen: state.crudForm.isSignInOpen,
      open: state.crudForm.open,
      isLoading: state.crudForm.isLoading,
      ...state
    }};
    
    const mapDispatchToProps = dispatch => ({
      switchSignInClick: bindActionCreators(switchSignInWindow, dispatch),
      switchWindow: bindActionCreators(switchUpdateWindow, dispatch),
      getAllTodos: bindActionCreators(getAllTodosRequest, dispatch)
     });
    
    export default connect(
      mapStateToProps,
      mapDispatchToProps
    )(ButtonAppBar);
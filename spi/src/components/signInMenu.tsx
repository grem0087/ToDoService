import React from 'react';
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux';
import { switchSignInWindow } from '../actions';

import { makeStyles } from '@material-ui/core/styles';
import Button from '@material-ui/core/Button';
import Avatar from '@material-ui/core/Avatar';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemAvatar from '@material-ui/core/ListItemAvatar';
import ListItemText from '@material-ui/core/ListItemText';
import DialogTitle from '@material-ui/core/DialogTitle';
import Dialog from '@material-ui/core/Dialog';
import GitHubIcon from '@material-ui/icons/GitHub';
import { GoogleLogin } from 'react-google-login';
import { blue, grey } from '@material-ui/core/colors';

const useStyles = makeStyles({
  google: {
    backgroundColor: blue[100],
    color: blue[600],
  },
  github: {
    backgroundColor: grey[300],
    color: grey[900],
  }
});

export interface SimpleDialogProps {
  isSignInOpen: boolean;
  selectedValue: string;
  
  switchWindow: (value: boolean) => void;
}

const responseGoogle = (response) => {
  sessionStorage.setItem("userData", JSON.stringify(response));
  console.log(response['tokenId']);
  console.log(response['profileObj'].email);
  console.log(response);
}

function SignInMenu(props: SimpleDialogProps) {
  const classes = useStyles();
  const { switchWindow, isSignInOpen } = props;

  const handleListItemClick = (value: string) => {
    console.log(value);
  };

  return (
    <Dialog onClose={()=>switchWindow(false)}  aria-labelledby="simple-dialog-title" open={isSignInOpen}>
      <DialogTitle id="simple-dialog-title">Sign In</DialogTitle>
      <List>
          <ListItem >
            <GoogleLogin
                  clientId="43180207284-c7cljolm8vsa20anmot6len0nm1av8qi.apps.googleusercontent.com"
                  buttonText="Log in with Google"
                  onSuccess={responseGoogle}
                  // onFailure={responseGoogle}
                  isSignedIn={true}
                  cookiePolicy={'single_host_origin'}
                />
          </ListItem>

          <ListItem button onClick={() => handleListItemClick("email")}>
            <ListItemAvatar>
              <Avatar className={classes.github}>
                <GitHubIcon />
              </Avatar>
            </ListItemAvatar>
            <ListItemText primary={"Log in with Github"} />
          </ListItem>
      </List>
    </Dialog>
  );
}

const mapStateToProps = (state, ownProps) => {
  return {
    isSignInOpen: state.crudForm.isSignInOpen,
    ...state
  }
};

const mapDispatchToProps = dispatch => ({
  switchWindow: bindActionCreators(switchSignInWindow, dispatch)
});

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(SignInMenu);
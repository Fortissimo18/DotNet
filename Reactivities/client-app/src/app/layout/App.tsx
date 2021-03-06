import React, { Fragment } from 'react';
import { Container } from 'semantic-ui-react';
import NavBar from './NavBar';
import ActivityDashboard from '../../features/activities/dashboard/ActivityDashboard';
import { observer } from 'mobx-react-lite';
import { Route, useLocation } from 'react-router-dom';
import ActivityForm from '../../features/activities/form/ActivityForm';
import HomePage from '../../features/activities/home/HomePage';
import ActivityDetails from '../../features/activities/details/ActivityDetails';

function App() {

  const location = useLocation();

  return (
    <Fragment>
      <Route exact path='/' component={HomePage} />
      <Route path={'/(.+)'} render={() => (
        <>
          <NavBar />
          <Container style={{ marginTop: '7em' }}>
            <Route exact path='/activities' component={ActivityDashboard} />
            <Route path='/activities/:id' component={ActivityDetails} />
            <Route key={location.key} path={['/manage/:id', '/createActivity']} component={ActivityForm} />
            {/* give a key for force reset component */}
          </Container>
        </>
      )} />

    </Fragment>
  );
}

export default observer(App);

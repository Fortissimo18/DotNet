import { observer } from 'mobx-react-lite';
import React, { useEffect } from 'react';
import { Grid } from 'semantic-ui-react';
import LoadingComponent from '../../../app/layout/LoadingComponent';
import { useStore } from '../../../app/stores/store';
import ActivityFilters from './ActivityFilters';
import ActivityList from './ActivityList';



export default observer(function ActivityDashboard() {

    const {activityStore} = useStore();
    const {loadActivities, activityRetristry} = activityStore;

    useEffect(() => {
      if (activityRetristry.size <= 1) loadActivities();
      },[activityRetristry.size, loadActivities]
  ) // the [] inside then is to ensure the query only runs once
  
  
    if (activityStore.loadingInitial) return <LoadingComponent content='Loading app' />

    return (
        <Grid>
            <Grid.Column width='10'>
                <ActivityList />
            </Grid.Column>
            <Grid.Column width='6'>
                <ActivityFilters />
            </Grid.Column>
        </Grid>
    )
})

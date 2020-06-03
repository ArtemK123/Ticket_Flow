import React from "react";
import Paper from "@material-ui/core/Paper";
import Box from "@material-ui/core/Box";
import { makeStyles } from "@material-ui/core/styles";
import Grid from "@material-ui/core/Grid";
import { Typography } from "@material-ui/core";
import { Divider } from "@material-ui/core";

const useStyles = makeStyles((theme) => ({
    root: {
        flexGrow: 1,
        px: 0
    },
    paper: {
        height: 250,
        width: 200,
    },
}));

function HomePage() {
    const styles = useStyles();

    return (
        <Box>
            <h3>HomePage</h3>
            <Box border={1} p={1}>
                <Grid container>
                    <Grid item container justify="space-between" xs={3}>
                        <Grid item>
                            <Typography>24 Jun</Typography>
                        </Grid>
                        <Grid item>
                            <Divider orientation="vertical"/>
                        </Grid>
                        <Grid item/>
                    </Grid>
                    <Grid item container xs={9} spacing={2}>
                        <Grid item><Paper className={styles.paper}/></Grid>
                        <Grid item><Paper className={styles.paper}/></Grid>
                        <Grid item><Paper className={styles.paper}/></Grid>
                        <Grid item><Paper className={styles.paper}/></Grid>
                    </Grid>
                </Grid>
            </Box>
            <Box border={1} p={1}>
                <Grid container>
                    <Grid item container justify="space-between" xs={3}>
                        <Grid item>
                            <Typography>24 Jun</Typography>
                        </Grid>
                        <Grid item>
                            <Divider orientation="vertical"/>
                        </Grid>
                        <Grid item/>
                    </Grid>
                    <Grid item container xs={9} spacing={2}>
                        <Grid item><Paper className={styles.paper}/></Grid>
                        <Grid item><Paper className={styles.paper}/></Grid>
                        <Grid item><Paper className={styles.paper}/></Grid>
                        <Grid item><Paper className={styles.paper}/></Grid>
                    </Grid>
                </Grid>
            </Box>
        </Box>
    );
}

export default HomePage;
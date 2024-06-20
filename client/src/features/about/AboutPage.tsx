import { Alert, AlertTitle, Button, ButtonGroup, Container, List, ListItem, ListItemText, Typography } from "@mui/material";
import agent from "../../app/api/agent";
import { useState } from "react";



export default function AboutPage() {
    const [validationErrors, setValidationErrors] = useState<string[]>([]);

    function getValidationError() {
        agent.TestErrors.getValidationError()
            .then(() => console.log('should not see this'))
            .catch(error => {
                setValidationErrors(error);
            })
    }

    return (
        <Container>
            <Typography gutterBottom variant="h2">errors for testing</Typography>
            <ButtonGroup fullWidth>
                <Button variant="contained" onClick={() => agent.TestErrors.get400Error().catch(error => console.log(error))}>test 400 error</Button>
                <Button variant="contained" onClick={() => agent.TestErrors.get401Error().catch(error => console.log(error))}>test 401 error</Button>
                <Button variant="contained" onClick={() => agent.TestErrors.get404Error().catch(error => console.log(error))}>test 404 error</Button>
                <Button variant="contained" onClick={() => agent.TestErrors.get500Error().catch(error => console.log(error))}>test 500 error</Button>
                <Button variant="contained" onClick={getValidationError}>test validation error</Button>

            </ButtonGroup>
            {validationErrors.length > 0 &&
                <Alert severity='error'>
                    <AlertTitle>Validation errors</AlertTitle>
                    <List>
                        {validationErrors.map((error) => (
                            <ListItem key={error}>
                                <ListItemText>{error}</ListItemText>
                            </ListItem>
                        ))}

                    </List>
                </Alert>
            }

        </Container>
    )
}